let map, infoWindow;//Google Map:global variable
let page = 1; //Paging:global variable


//Google Map Function
function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 6,
        mapId: 'ca3e1b773e1fd477',
    });
    infoWindow = new google.maps.InfoWindow();

    const locationButton = document.createElement("button");
    //locationButton.textContent = "Pan to Current Location";
    locationButton.classList.add("custom-map-control-button");
    locationButton.innerHTML = "<img src='https://i.ibb.co/FnJMDQp/zerohungerlogotransparent.png' style='margin:0px 0px 0px 0px;' alt='Your Location' width='100' height='100'>";
    locationButton.style.marginTop = "0px";
    locationButton.style.padding = '0px 0px 0px 0px';
    
    locationButton.style.backgroundColor = "transparent";
    locationButton.style.border = 'unset';
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(locationButton);
    locationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };

                    infoWindow.setPosition(pos);
                    infoWindow.setContent("Location found.");
                    infoWindow.open(map);
                    map.setCenter(pos);
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    });
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}

function getLatLon() {
    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                };

                infoWindow.setPosition(pos);
                infoWindow.setContent("Location found.");
                infoWindow.open(map);
                map.setCenter(pos);
            },
            () => {
                handleLocationError(true, infoWindow, map.getCenter());
            }
        );
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }

}
//End of Google Map Function

//Calculate cooked food distance
var options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
};

function success(pos) {
    var count = document.getElementById('itemCount').innerHTML;
    count = parseInt(count);
    var crd = pos.coords;
    for (var i = 0; i < count; i++) {
        if (document.getElementsByName('itemLat')[i] != null) {
            var itemLat = document.getElementsByName('itemLat')[i].innerHTML;
            var itemLon = document.getElementsByName("itemLon")[i].innerHTML;
            itemLat = parseFloat(itemLat);
            itemLon = parseFloat(itemLon);
            var x = distance(itemLat, itemLon, crd.latitude, crd.longitude);
            x = x.toFixed(2);
            document.getElementsByName('itemDistance')[i].innerHTML = x + "km";
            document.getElementsByName('itemDistance')[i].style.fontWeight = "bold";
            console.log(crd.latitude);
            console.log(crd.longitude);
            }
    
    }
    pageLoading();
}

function error(err) {
    console.warn(`ERROR(${err.code}): ${err.message}`);
}

function distance(lat1, lon1, lat2, lon2) {


    if ((lat1 == lat2) && (lon1 == lon2)) {
        return 0;
    }
    else {
        var radlat1 = Math.PI * lat1 / 180;
        var radlat2 = Math.PI * lat2 / 180;
        var theta = lon1 - lon2;
        var radtheta = Math.PI * theta / 180;

        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
        if (dist > 1) {
            dist = 1;
        }
        dist = Math.acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        dist = dist * 1.609344
        return dist;
    }
}
//End Function Calculate Cookfood distance

//Display promptBox
function displayItemBox(sn, fn, q,  id) {

    var d = document.getElementById('itemDistance.' + id).innerHTML;
    var box = document.getElementById('promptContainer');
    box.style.display = "flex";
    document.getElementById('pFoodShopName').innerHTML = sn;
    document.getElementById('pFoodName').innerHTML = fn;
    document.getElementById('pFoodQuantity').innerHTML = q;
    document.getElementById('pFoodDistance').innerHTML = d;
    document.getElementById('pFoodId').value = id;
    if (q == 0) {
        document.getElementById('reservedBut').disabled = true;
    }
    else {
        document.getElementById('reservedBut').disabled = false;
    }
}
//End of Prompt Box Function


//Paging Function

//Called when html loaded
function pageLoading() {
    document.getElementById('pPage').disabled = 'true';
    var x = document.getElementsByName('itemContainer');
    for (var i = 0; i < 4; i++)
    {
        if (x[i] != null)
        { x[i].style.display = "flex"; }
    }
}

//call when client click next page
function nextPage()
{
    page = page + 1;
    var n = page;
    var pageSize = 4
    var x = document.getElementsByName('itemContainer');
    
        for (var i = n * 4; i <=(n + 1) * 4 - 1; i++) {
            if (x[i - pageSize - 4] != null)
            {
                x[i - pageSize-4].style.display = "none";
            }
            if (x[i - pageSize] != null)
            {
                x[i-pageSize].style.display = "flex";
            }
            
            
            //disable next page button
            if (Math.ceil((x.length / 4)) == page)
            {
                console.log(Math.ceil((x.length / 4)));
                (document.getElementById('nPage')).disabled = true;
            }
            
                (document.getElementById('pPage')).disabled = false;
            
            
    }
    (document.getElementById('pPage')).disabled = false;
    return false;
    }


//called when client click previous page
function previousPage() {
    var n = page;
    var x= document.getElementsByName('itemContainer');
    var pageSize = 4;
    for (var i = n * 4 - 1; i >= ((n - 1) * 4); i--) {
        console.log("hide " + i);
        console.log("displauy "+(i-pageSize))
        if (x[i] != null) {
        x[i].style.display = 'none';
        
        }
        if (x[i-pageSize] != null) {
            x[i - pageSize].style.display = 'flex';
        }
    }
    page = page - 1;
    //Disable previous page button
    if (page == 1) {
        document.getElementById('pPage').disabled = true;

    }
    document.getElementById('nPage').disabled = false;
}

//End of Paging Function