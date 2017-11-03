//  JavaScript
var cordova = require('cordova'),
    ToUpperPlugin= require('./UdpDataResponsePlugin');

module.exports = {

    GetUdpData: function(successCallback, errorCallback ,[mensage,serverPort]){
      
    
        try {
            var p = UdpDataResponse.UdpDataResponse.getudpdata(mensage, serverPort);
            p.done(function(v){

                if(v != 'NO_RESPONSE' || v != 'PORT_OCCUPIED' ){
                    successCallback(v);
                    
                }else{
                    errorCallback(v);
                }
            }); 
       
        } catch(error) {
            errorCallback(error.mensage);
        }
    }
};

require("cordova/exec/proxy").add("UdpDataResponsePlugin", module.exports);