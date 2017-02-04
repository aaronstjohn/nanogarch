var Promise = require("bluebird");
var join = Promise.join;
var express = require('express');
var solc = require('solc');
var fs = Promise.promisifyAll(require("fs"));
var prettyjson = require('prettyjson');
var jsonQuery = require('json-query');
var Web3 = require('web3');
var Config = require("truffle-config");

var app = express();
var options = {
  noColor: true
};
var truffle_options = {
  logger: console
};
function getAccounts(web3, config) 
{
    return new Promise(function(accept, reject) {
      web3.eth.getAccounts(function(err, accs) {
        if (err) return reject(err);
        accept(accs);
      });
    });
}
// var config = Config.detect(truffle_options,"./truffle.js");
// console.log("CONFIG: "+config.provider);
// var config = Config.default().merge(truffle_options);
var web3 = new Web3();
// console.log("ATTEMPTING TO USE PROVIDER: "+config);
// MyContract.setProvider(new Web3.providers.HttpProvider("http://localhost:8545"));

web3.setProvider(new Web3.providers.HttpProvider("http://localhost:8545"));
getAccounts(web3).then((accs)=>{
    console.log("GOT ACCOUNTS: "+accs);
});
// web3.eth.getAccounts(function(err, accs) {
//     if (err != null) {
//         alert("There was an error fetching your accounts.");
//         return;
//     }

//     if (accs.length == 0) {
//         alert("Couldn't get any accounts! Make sure your Ethereum client is configured correctly.");
//         return;
//     }});
const contractsFolder = './contracts/';
const moduleFolder = './build/contracts/';

//Read in all the contract source code 
var sources= Promise.all(
    fs.readdirAsync(contractsFolder)
        .map( (fileName) =>{
        return fs.readFileAsync(contractsFolder+fileName,'utf8')
                .then(function(data){
                    return {fileName:fileName,contents:data};
                })
                .catch(function ignore() {});
   
        }))
        .reduce( (allFiles,item)=>{
            allFiles[item.fileName] = item.contents; 
            return allFiles;
        },{});
//Import all the modules for the contracts 
var modules= Promise.all(
    fs.readdirAsync(moduleFolder)
        .map((fileName) =>{
            var modulePath = moduleFolder+'/'+fileName; 
            return {fileName:fileName,module:require(modulePath)};
            
    
        }))
    .reduce((allModules,item)=>{
        allModules[item.fileName] = item.module; 
        return allModules;
    },{});

join(sources,modules,(allSources,allModules)=>{
    var output = solc.compile({sources: allSources});
    if(output.errors)
    {
        console.log(prettyjson.render(output, options));
        return;
    }
    for (var contractName in output.contracts)
    {
        var sourceFileName = contractName+".sol";
        var moduleFileName = contractName+".sol.js";

        var abiData = JSON.parse(output.contracts[contractName].interface);
        //Get the syntax tree to find the library attribute 
        var ast=output.sources[sourceFileName].AST;

        //Query to see if this contract is a library 
        var isLibrary = jsonQuery('children.attributes[isLibrary]',{data:ast}).value[0];
        
        //Ignore libraries 
        if(isLibrary)
            continue;
        var contractModule = allModules[moduleFileName];
        abiData.forEach(function(entry){
            
            if(entry.type=="function")
            {
                bindContractEndpoint(contractName,entry,contractModule);
            }
        });
    }
});
function bindContractEndpoint(contractName,abiFunctionData,contractModule)
{   
    var path = '/'+contractName+'/'+abiFunctionData.name;
    app.get(path,handleContract.bind(null,contractName,abiFunctionData,contractModule));
    console.log("Created endpoint: "+path);
}
function handleContract(contractName,abiFunctionData,contractModule,req,res)
{

    res.end(""+contractName+"/"+abiFunctionData.name);
}
// app.get('/blah/listUsers', function (req, res) {
// //    fs.readFile( __dirname + "/" + "users.json", 'utf8', function (err, data) {
// //        console.log( data );
// //        res.end( data );
// //    });
//     res.end( "data" );
// })

// var server = app.listen(8081, function () {

//   var host = server.address().address
//   var port = server.address().port

//   console.log("Example app listening at http://%s:%s", host, port)

// });

// Promise.all(files)
//     .then(function(allFiles){
//         var input={};
//         var interface = {};
//         allFiles.forEach((file)=>{input[file.fileName]=file.contents;});
//         var output = solc.compile({sources: input});
        
//         if(output.errors)
//         {
//             console.log(prettyjson.render(output, options));
//             return;
//         }
//         for (var contractName in output.contracts)
//         {
//             var abiData = JSON.parse(output.contracts[contractName].interface);
//             //Get the syntax tree to find the library attribute 
//             var ast=output.sources[contractName+".sol"].AST;

//             //Query to see if this contract is a library 
//             var isLibrary = jsonQuery('children.attributes[isLibrary]',{data:ast}).value[0];
            
//             //Ignore libraries 
//             if(isLibrary)
//                 continue;
//             abiData.forEach(function(entry){
                
//                 if(entry.type=="function")
//                 {
//                     // console.log("Found function: "+contractName+"/"+entry.name);
//                     bindContractEndpoint(contractName,entry);
//                 }
//             });
           

//         }
// });



 