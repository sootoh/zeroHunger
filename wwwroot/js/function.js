let map, infoWindow;//Google Map:global variable
let page = 1; //Paging:global variable

let biggest = 0;
let distanceList = [];
//Google Map Function
function initMaps() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: 3.8925533126426664, lng: 102.10080302222984 },
        zoom: 8, 
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


    //setMarker(map);
    var y = document.getElementsByName('itemLat');
    const img =
    {
        url: "../images/markericon.png",
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(32, 32),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 32),
        scaledSize: new google.maps.Size(25, 25)
    };
    for (var i = 0; i < y.length; i++) {
        var x = { lat: parseFloat(document.getElementsByName('itemLat')[i].innerHTML), lng: parseFloat(document.getElementsByName('itemLon')[i].innerHTML) };
        console.log(x);
        var marker = new google.maps.Marker(
            {
                position: x,
                map,
                title: document.getElementsByName('itemName')[i].innerHTML,
                icon:img,
            });
        var ifw = new google.maps.InfoWindow({
            content: document.getElementsByName('itemName')[i].innerHTML,
        });
        ifw.addListener("click", () => {
            ifw.open({
                anchor: marker,
                map,
                shouldFocus: false,
            })
        });

    }
    
    //Rearrange
    

}

function initMapr() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: 3.8925533126426664, lng: 102.10080302222984 },
        zoom: 8,
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

    //setMarker(map);
    const images =
    {
        url: "../images/reservedmarker.png",
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(60, 60),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 32),
        scaledSize: new google.maps.Size(25, 25)
    };
 
        var x = { lat: parseFloat(document.getElementsByName('itemLat')[0].value), lng: parseFloat(document.getElementsByName('itemLon')[0].value) };
        const marker=new google.maps.Marker(
            {
                position: x,
                map,
                title: t,
                icon:images
            });
    console.log(x);
    var t="Your Food Is Here"
        const ifw=new google.maps.InfoWindow({
            content: t,
        });
        ifw.addListener("click", () => {
            ifw.open({
                anchor: marker,
                map,
                shouldFocus: false,
            })
        });
    const flocationButton = document.createElement("button");
    //locationButton.textContent = "Pan to Current Location";
    flocationButton.classList.add("custom-map-control-button");
    flocationButton.innerHTML = "<img src='../images/reservedmarker.png' style='margin:10px 10px 10px 10px;' alt='FoodLocation' width='50' height='50'>";
    flocationButton.style.marginTop = "10px";
    flocationButton.style.padding = '10px 10px 10px 10px';

    flocationButton.style.backgroundColor = "transparent";
    flocationButton.style.border = 'unset';
    map.controls[google.maps.ControlPosition.LEFT_TOP].push(flocationButton);
    flocationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const pos = {
                        lat: parseFloat(document.getElementsByName('itemLat')[0].value),
                        lng: parseFloat(document.getElementsByName('itemLon')[0].value),
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

    //Rearrange


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


function GoLocation(lat,lon) {
    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const pos = {
                    lat: lat,
                    lng: lon,
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
            distanceList.push([i, x]);
            
            document.getElementsByName('itemDistance')[i].innerHTML = x + "km";
            document.getElementsByName('itemDistance')[i].style.fontWeight = "bold";
            }
    
    } sort();
    //SortDistance();
    pageLoading();
}
function sort() {

    
    
    for (var j = 0; j < distanceList.length; j++) {
        smallest = j;
        for (var i = j; i < distanceList.length ; i++) {
        if (distanceList[i][1] < distanceList[smallest][1]) {
            smallest = i;
        }

        }
        var tem = distanceList[j];
        
        distanceList[j] = distanceList[smallest];
        distanceList[smallest] = tem;
    }
    for (x of distanceList) {
        
    }
    
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


function closeRePrompt()
{
    var box = document.getElementById('promptContainer');
    box.style.display = "none";
}
//End of Prompt Box Function


//Paging Function

//Called when html loaded
function pageLoading() {
    document.getElementById('pPage').disabled = 'true';
    var x;
    for (var i = 0; i < 4; i++)
    {
        if (distanceList[i] != null) {
        if (document.getElementsByName('itemContainer')[distanceList[i][0]] != null)
        {
            x = document.getElementsByName('itemContainer')[distanceList[i][0]];
            x.style.display = "flex";
            x.style.order = i;
            }
        }
    }
    if (distanceList.length < 4) {
        (document.getElementById('nPage')).disabled = true;
    

    (document.getElementById('pPage')).disabled = true;
    }
    
    
   
}

//call when client click next page
function nextPage()
{
    page = page + 1;
    var n = page;
    var pageSize = 4
    var x = distanceList;
    
    for (var i = n * 4; i <= (n + 1) * 4 - 1; i++) {
        
            if (document.getElementsByName('itemContainer')[x[i - pageSize - 4][0]] != null)
            {
                document.getElementsByName('itemContainer')[x[i - pageSize - 4][0]] .style.display = "none";
            }

        if (x[i-pageSize]!=null)
            {
                if (document.getElementsByName('itemContainer')[x[i - pageSize][0]] != null) {
                    document.getElementsByName('itemContainer')[x[i - pageSize][0]].style.display = "flex";
                }
            }
            
            
            //disable next page button
            if (Math.ceil((x.length / 4)) == page)
            {
                
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
    var x = distanceList;
    var pageSize = 4;
    for (var i = n * 4 - 1; i >= ((n - 1) * 4); i--) {

        if (x[i] != null) {
            if (document.getElementsByName('itemContainer')[x[i][0]] != null) {
                document.getElementsByName('itemContainer')[x[i][0]].style.display = 'none';

            }
        }
        if (x[i - pageSize] != null) {
            if (document.getElementsByName('itemContainer')[x[i - pageSize][0]] != null) {
                document.getElementsByName('itemContainer')[x[i - pageSize][0]].style.display = 'flex';
            }
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

//Google Map Marker
function setMarker(m) {
   
}
function Change(hide, show) {
    hide.className= "";
    show.className= "ReservationTitle";
    console.log("hello");
    document.getElementById(hide.id + 'Container').style.display = "none";
    document.getElementById(show.id + 'Container').style.display = "flex";
    



}