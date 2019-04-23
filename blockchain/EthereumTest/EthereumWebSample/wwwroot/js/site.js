// Write your Javascript code.
window.addEventListener('load', function () {

    // Checking if Web3 has been injected by the browser (Mist/MetaMask)
    if (typeof web3 !== 'undefined') {
        // Use Mist/MetaMask's provider
        window.web3 = new Web3(web3.currentProvider);
    } else {
        alert('No web3? You should consider trying MetaMask!')
        // fallback - use your fallback strategy (local node / hosted node + in-dapp id mgmt / fail)
        window.web3 = new Web3(new Web3.providers.HttpProvider("http://localhost:62166"));
    }

    // Now you can start your app & access web3 freely:
    //startApp()

    web3.version.getNetwork((err, netId) => {
        switch (netId) {
            case "1":
                console.log('This is mainnet')
                break
            case "2":
                console.log('This is the deprecated Morden test network.')
                break
            case "3":
                console.log('This is the ropsten test network.')
                break
            default:
                console.log('This is an unknown network.')
        }
    })

    //web3.eth.sendTransaction({ to: "0xE3DA15Dd29678c0206644ed12E74943eC13bf364", value: web3.toWei('10', 'ether')}, function (err, transactionHash) {
    //    if (!err)
    //        console.log(transactionHash); // "0x7f9fade1c0d57a7af66ab4ead7c2eb7b11a91385"
    //});
})

var AbiOfContract = JSON.parse('[{"constant":false,"inputs":[{"name":"theValue","type":"uint256"}],"name":"writeToResult","outputs":[],"payable":false,"stateMutability":"nonpayable","type":"function"},{"constant":true,"inputs":[{"name":"valueFirst","type":"uint256"},{"name":"valueSecond","type":"uint256"}],"name":"sum","outputs":[{"name":"","type":"uint256"}],"payable":false,"stateMutability":"pure","type":"function"},{"constant":true,"inputs":[],"name":"resultValue","outputs":[{"name":"","type":"uint256"}],"payable":false,"stateMutability":"view","type":"function"}]');
var contractAbi = window.web3.eth.contract(AbiOfContract);

submitTrans = function () {

    //var estimatedGass = await theFunction.EstimateGasAsync(acc.PublicAddress, new HexBigInteger(new BigInteger(3000000)), null, schoolName);
    var Contractaddress = document.getElementById('ctrAddress').value;
    var numberToWrite = document.getElementById('numberToWrite').value;
    var Accountaddress = web3.Accountaddress;

    var myContract = contractAbi.at(Contractaddress);
    // suppose you want to call a function named myFunction of myContract
    var getData = myContract.writeToResult.getData(numberToWrite);
    //finally paas this data parameter to send Transaction
    window.web3.eth.sendTransaction({ to: Contractaddress, from: Accountaddress, data: getData }, function (err, transactionHash) {
        if (!err)
            console.log(transactionHash);
    });
}

submitSumTrans = function () {
    var Contractaddress = document.getElementById('ctrAddress').value;
    var sumNumber1 = document.getElementById('sumNumber1').value;
    var sumNumber2 = document.getElementById('sumNumber2').value;
    var Accountaddress = web3.Accountaddress;

    var myContract = contractAbi.at(Contractaddress);

    //var getData = myContract.sum.getData(sumNumber1, sumNumber2);
    //window.web3.eth.sendTransaction({ to: Contractaddress, from: Accountaddress, data: getData }, function (err, transactionHash) {
    //    if (!err)
    //        console.log(transactionHash);
    //});

    myContract.sum.call(sumNumber1, sumNumber2, function (err, res) {
        document.getElementById('resultSumValue').value = res;
    });

}

function obtainVariable() {
    var Contractaddress = document.getElementById('ctrAddress').value;

    var Accountaddress = web3.Accountaddress;
    if (Accountaddress == 0) {
        document.getElementById('resVal').value = "<Ensure MetaMask is connected to the right network!>";
    } else {
        if (Contractaddress != 0) {

            var myContract = contractAbi.at(Contractaddress);

            myContract.resultValue.call(function (err, res) {
                document.getElementById('resVal').value = res;
                console.log('Obtained value: ' + res);
            });
        } else {
            document.getElementById('resVal').value = "<Fill contract address>";
        }
    }
}

window.setInterval(obtainVariable, 1000);