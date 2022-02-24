<!doctype html>
<html lang="en">
<head>
    <script src="Scripts/vendors.js" type="text/javascript"></script>
    <script src="Scripts/app.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ocdailym62hf.crm.dynamics.com/webresources/Widget/msdyn_ciLibrary.js" data-crmurl="https://ocdailym62hf.crm.dynamics.com" data-cifid="CIFMainLibrary">
     </script>
    <script>
        (function () {
            window.addEventListener("CIFInitDone", function () {
                ciLoaded = true;
                Microsoft.CIFramework.setMode(1);
                displayPresence = function (presence) {
                    document.getElementById("custompresence").value = presence;
                    document.getElementById("livePresence").innerHTML = presence;
                    let color = "grey";
                    if (presence == "Available") 
                        color = "green";
                    else if (presence == "Busy")
                        color = "red";
                    else if (presence == "Appear away")
                        color = "yellow";
                    else if (presence == "Do not disturb")
                        color = "brown";
                    else if (presence == "OnVoicecall")
                        color = "brown";
                    document.getElementById("presenceOnChange").style.color = color;
                    document.getElementById("presenceOnChange").style.background = color;
                }
                handlerFunction = function (eventData) {
                    var obj = JSON.parse(eventData);
                    var presence = obj.presenceInfo.presenceText;
                    displayPresence(presence)
                }
                window.Microsoft.CIFramework.addHandler("onPresenceChange", handlerFunction);
            });

        })();

    </script>
</head>
<body clientAppType="NiniWidget">
    <div id="iframeContainer">
        <style>
            .dot {
                height: 25px;
                width: 25px;
                border-radius: 50%;
                display: inline-block;
            }
        </style>
        <p></p>
        <span class="dot" id="presenceOnChange"></span>
        <p id="livePresence"></p>
        <p></p>
        <p></p>
        <br>
        <br>
        <button id="call" type="button" onclick="setDND()">
            Accept call
        </button>
        <script>
            function setDND() {
                Microsoft.CIFramework.setPresence("OnVoicecall");
                document.getElementById("call").style.display = "none";
                document.getElementById("end").style.display = "block";
            }
        </script>
        <button id="end" style="display:None" type="button" onclick="releasePresence()">
            End Call
        </button>
        <script>
            function releasePresence() {
                Microsoft.CIFramework.setPresence("Available");
                document.getElementById("end").style.display = "None";
                document.getElementById("call").style.display = "block";
            }
        </script>
        <br>
        <br>
        <br>
        <br>
        <br>
        <form>
            Select the presence to be set:
            <select id="custompresence">
                <option value="Available">Available</option>
                <option value="Busy">Busy</option>
                <option value="Do not disturb">Do not disturb</option>
                <option value="Appear away">Appear away</option>
            </select>
        </form>
        <button type="button" onclick="setPresence()">Set Presence</button>
        <p id="setPresenceText"></p>
        <script>
            function setPresence() {
                var custompresence = document.getElementById("custompresence").value;
                return new Promise((resolve, reject) => {
                    Microsoft.CIFramework.setPresence(custompresence).then(
                        function (result) {
                            if(!result)
                            document.getElementById("setPresenceText").innerHTML = "OC Presence is in error state";

                        },
                        function (error) {

                            document.getElementById("setPresenceText").innerHTML = "ERROR";
                            reject(error);
                        });

                });
            }
        </script>
        <br>
        <br>
        <br>
        <button type="button" onclick=" getPresence()">
            Click me to display OC presence.
        </button>
        <p id="presenceOnChangeText"></p>
        <script type="text/javascript">

            function getPresence() {
                return new Promise((resolve, reject) => {
                    Microsoft.CIFramework.getPresence().then(
                        function (result) {
                            if (result == "FAILED")
                                document.getElementById("presenceOnChangeText").innerHTML = "OC Presence is in error state";
                            else {
                                document.getElementById("presenceOnChangeText").innerHTML = result;
                                displayPresence(result);
                            }
                            return result;

                        },
                        function (error) {

                            document.getElementById("presenceOnChangeText").innerHTML = "ERROR";
                            reject(error);
                        });

                });
            }
            function getResult() {
                getPresenceOnClick().then(function (response) {
                    return response;
                })
            }
        </script>
    </div>
</body>
</html>