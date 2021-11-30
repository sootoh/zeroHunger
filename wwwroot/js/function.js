let map, infoWindow;
let ulat, ulng;
function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 6,
    });
    infoWindow = new google.maps.InfoWindow();

    const locationButton = document.createElement("button");

    locationButton.textContent = "Pan to Current Location";
    locationButton.classList.add("custom-map-control-button");
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);
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
    for (var i =0; i < count; i++) {
    console.log("Function Called");
    var itemLat = document.getElementsByName('itemLat')[i].innerHTML;
    var itemLon = document.getElementsByName("itemLon")[i].innerHTML;
    itemLat = parseFloat(itemLat);
    itemLon = parseFloat(itemLon);
    var x = distance(itemLat, itemLon, crd.latitude, crd.longitude);
    x =x.toFixed(2);
        document.getElementsByName('itemDistance')[i].innerHTML = x + "km";
        document.getElementsByName('itemDistance')[i].style.fontWeight="bold";
    console.log(itemLat);
    console.log(itemLon);
    console.log(crd.latitude);
        console.log(crd.longitude);
        
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

function displayItemBox(sn,fn,q,id)
{

    console.log(document.getElementById('itemDistance.' + id))
    console.log('itemDistance.' + id)
    var d = document.getElementById('itemDistance.' + id).innerHTML;
    console.log(document.getElementById('itemDistance.' + id))
    console.log('itemDistance.' + id)
    var box = document.getElementById('promptContainer');
    box.style.display = "flex";
    box.innerHTML = "";

    var form = document.createElement("form");
    form.method = "Post";
    

    var propTitle = document.createElement("div");
    propTitle.class = "promptTitle";
    propTitle.innerHTML = "Donation";

    var table = document.createElement("table");
    //Food Shop Name Row
    var shopRow = document.createElement("tr");
    shopRow.innerHTML = "<th class='tableLeft' >Shop Name</th><td id = 'shopName' class='tableLeft' >" + sn + "</td >"
    //Food Name Row
    var foodRow = document.createElement("tr");
    foodRow.innerHTML = "<th class='tableLeft' >Food Name</th><td id = 'pFoodName' class='tableLeft' >" + fn + "</td >";
    //Food Quantity Row
    var quantityRow = document.createElement("tr");
    quantityRow.innerHTML = "<th class='tableLeft'>Quantity</th><td class='tableLeft' >" + q + "</td >";
    //Food Distance Row
    var distanceRow = document.createElement("tr");
    distanceRow.innerHTML = "<th class='tableLeft'>Distance</th> <td class='tableLeft' >" + d + "</td >";
    //Button Row
    var buttonRow = document.createElement("tr");
    buttonRow.innerHTML = "<td colspan='2' style='text-align:center;'> <input type='submit' value='Reserve'>"
    
    table.appendChild(shopRow);
    table.appendChild(foodRow);
    table.appendChild(quantityRow);
    table.appendChild(distanceRow);
    table.appendChild(buttonRow);

    
    box.appendChild(propTitle);
    box.appendChild(form);
    form.appendChild(table);
    
}