/*make sure html is processed by browser and save references to all reqired referenecs here*/
window.addEventListener('DOMContentLoaded', () => {

	const resetBoard = () => {
    	//sets the board back to empty
        board = ['', '', '', '', '', '', '', '', ''];
        isGameActive = true;
        announcer.classList.add('hide'); //hides previous result

        if (currentPlayer === 'O') {
            changePlayer();
        }

        tiles.forEach(tile => {
            tile.innerText = '';
            tile.classList.remove('playerX');
            tile.classList.remove('playerO');
        });
    }

	const tiles = Array.from(document.querySelectorAll('.tile')); //converts to a proper array for easier manipulations
    const playerDisplay = document.querySelector('.display-player');
    const resetButton = document.querySelector('#reset');
    const announcer = document.querySelector('.announcer');

    resetButton.addEventListener('click', resetBoard);

    let board = ['', '', '', '', '', '', '', '', '']; //array of 9 empty strins to represent board
    let currentPlayer = 'X'; //current player is eaier X or O
    let isGameActive = true;

    //end game state 
    const PLAYERX_WON = 'PLAYERX_WON';
    const PLAYERO_WON = 'PLAYERO_WON';
    const TIE = 'TIE';


    //win conditions here
    const winningConditions = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];

    function handleResultValidation() {
    	/* Check if we have winner by looping through wincon array and seeing if we have a match */
        let roundWon = false;
        for (let i = 0; i <= 7; i++) {
            const winCondition = winningConditions[i];
            const a = board[winCondition[0]];
            const b = board[winCondition[1]];
            const c = board[winCondition[2]];
            if (a === '' || b === '' || c === '') {
                continue;
            }
            if (a === b && b === c) {
                roundWon = true;
                break;
            }
        }

    	if (roundWon) {
            announce(currentPlayer === 'X' ? PLAYERX_WON : PLAYERO_WON);
            isGameActive = false;
            return;
        }

    	if (!board.includes('')){
        	announce(TIE);
    	}
    }

    

    const announce = (type) => {
    	//announce winner/end game state to users 
        switch(type){
            case PLAYERO_WON:
                announcer.innerHTML = 'Player <span class="playerO">O</span> Won';
                break;
            case PLAYERX_WON:
                announcer.innerHTML = 'Player <span class="playerX">X</span> Won';
                break;
            case TIE:
                announcer.innerText = 'Tie';
        }
        announcer.classList.remove('hide'); //removes hide class and shows announcement to user
    };

    
    const isValidAction = (tile) => {
    	//checks if action is value by seeing if tile has a value already + players only play empty tiles 
        if (tile.innerText === 'X' || tile.innerText === 'O'){
            return false;
        }

        return true;
    };


    const updateBoard =  (index) => {
        board[index] = currentPlayer;
    }


    const changePlayer = () => {
     	/*remove current player the change player to X if O and vice versa */

        playerDisplay.classList.remove(`player${currentPlayer}`);
        currentPlayer = currentPlayer === 'X' ? 'O' : 'X';
        playerDisplay.innerText = currentPlayer;
        playerDisplay.classList.add(`player${currentPlayer}`);
    }



    
    const userAction = (tile, index) => {
    	//called when user clicks on a tile
    	/*checks if the game is active and if yes then sent player X or O to fill grid space*/
        if(isValidAction(tile) && isGameActive) {
            tile.innerText = currentPlayer;
            tile.classList.add(`player${currentPlayer}`);
            updateBoard(index);//updates board array
            handleResultValidation();//checks for a winner
            changePlayer(); //change player
        }
    }

    

    //when we click on  tile a user action modifying the UI will be called
     tiles.forEach( (tile, index) => {
        tile.addEventListener('click', () => userAction(tile, index));
    });



});