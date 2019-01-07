pragma solidity ^0.4.18;

contract ExampleContract {
    uint public resultValue;

    function writeToResult(uint theValue) public {
        resultValue = theValue;
    }

    function sum(uint valueFirst, uint valueSecond) public pure returns (uint) {
        return valueFirst + valueSecond;
    }
}
