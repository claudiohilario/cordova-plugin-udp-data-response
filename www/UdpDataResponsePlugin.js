// JavaScript
var UdpDataResponse = {
    GetUdpData: function(successCallback, errorCallback, mensage, serverPort) {
        cordova.exec(successCallback,errorCallback,"UdpDataResponsePlugin","GetUdpData",[mensage, serverPort]);
    }
}

module.exports = UdpDataResponse;