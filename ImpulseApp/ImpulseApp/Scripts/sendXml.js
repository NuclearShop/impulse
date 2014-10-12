function send(block) {
   /* var xml = '' +
'<?xml version=\'1.0\' encoding=\'UTF-8\'?>' +
'<ad>' +
'<body>' +
'<![CDATA['+block+']]>' +
    '</body></ad>';*/
    var xml = document.createElement("XML");
    var ad = xml.appendChild(document.createElement("ad"));

    var userInfo = getUserInfo();
    ad.appendChild(userInfo);

    var body = document.createElement("body");
    body.appendChild(document.createTextNode(_(block).escape()));
    ad.appendChild(body);

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "http://localhost:56596/UserFront/CreateExecuting", true);
    xmlhttp.send(xml.innerHTML);
    xmlhttp.onreadystatechange = function (result) {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var locstring = result.target.response;
            var loc = JSON.parse(locstring);

            location.href = loc.redirectToUrl;
        }
    }
}

function getUserInfo() {
    var userInfo = document.createElement("userInfo");
    var language = document.createElement("language");
    language.appendChild(document.createTextNode(navigator.language));
    userInfo.appendChild(language);
    
    $.getJSON("http://api.hostip.info/get_json.php", function (data) {
        $.each(data, function (key, val) {
            var xmlNode = document.createElement(key);
            xmlNode.appendChild(document.createTextNode(val));
            userInfo.appendChild(xmlNode);
            
        });
    });
    return userInfo;
}