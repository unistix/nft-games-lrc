window.onload = function() {
	const word_ = document.getElementById('word');
	const wrongLetters_ = document.getElementById('wrong-letters');
	const playAgainBtn = document.getElementById('play-button');
	const popup = document.getElementById('popup-container');
	const notification = document.getElementById('notification-container');
	const finalMessage = document.getElementById('final-message');


	const figureParts= document.querySelectorAll(".figure-part");

	const words =['bitcoin','ethereum','polygon','immutable','loopring','blockchain','coin','token','dapp','gas','hodl','hash','wallet','staking','node','nft']

	let selectedWord = words[Math.floor(Math.random() * words.length)];
	

	const correctLetters = [];
	const wrongLetters = [];

	var gameOver = false;

	//Show hidden word
	function displayWord(){
	    word_.innerHTML = `
	    ${selectedWord
	    .split('')
	    .map(
	        letter =>`
	        <span class="letter">
	        ${correctLetters.includes(letter) ? letter : ''}
	        </span>
	        `
	    )
	    .join('')}
	    `;

	    const innerWord = word_.innerText.replace(/\n/g, '');

	    const winnerWord = innerWord.split(' ').join('')
	    
	    
	

	    if(winnerWord === selectedWord){
	    	/*gameOver = true;*/
	    	/*console.log(innerWord);
	    	console.log(selectedWord);*/
	    	gameOver = true;
	        finalMessage.innerText = 'You Win!';
	        
	        popup.style.display= 'flex';
	    }
	}

	displayWord()
	word_length = words.map(element => element.length)
	

	function updateWrongLetters(){
		wrongLetters_.innerHTML = `
	    ${wrongLetters.length > 0 ? '<p>Wrong</p>' : ''}
	    ${wrongLetters.map(letter => `<span>${letter}\n</span>`)}
	    `;

	    //Display parts
	    figureParts.forEach((part,index) => {
	        const errors = wrongLetters.length;

	        if(index < errors) {
	            part.style.display = 'block'
	        }
	        else{
	            part.style.display = 'none';
	        }
	    });

	    //Check if lost
	    if(wrongLetters.length === figureParts.length){
	        finalMessage.innerText = 'You Lose';
	        gameOver = true;
	        console.log("gameover")
	        console.log(gameOver)
	        /*window.addEventListener("keydown",  =>{
	   		 if(e.keyCode >= 65 && e.keyCode <=90){
	        	console.log("please reset")
	        }*/
	    }


	}

	function showNotification(){
	    //notification.classList.add('show');
	    notification.style.visibility = "visible" ;
	    


	    setTimeout(() => {
	        notification.style.visibility = "hidden" ;
	    }, 2000);


	}

	window.addEventListener('keydown', e =>{
	    if(e.keyCode >= 65 && e.keyCode <=90 && !gameOver){
	    	console.log(gameOver)
	        const letter = e.key;

	        if(selectedWord.includes(letter)){
	            if(!correctLetters.includes(letter)){
	                correctLetters.push(letter);

	                displayWord();
	            } else{
	                showNotification();
	            }
	        } else{
	            if(!wrongLetters.includes(letter)){
	                wrongLetters.push(letter);

	                updateWrongLetters();
	            } else{
	                showNotification();
	                /*console.log("debug")*/
	            }
	        }
	    }
	});

	playAgainBtn.addEventListener('click', () => {
    //Empty arrays
	    correctLetters.splice(0);
	    wrongLetters.splice(0);

	    selectedWord = words[Math.floor(Math.random() * words.length)];

	    displayWord();

	    wrongLetters_.innerHTML = ``
	    finalMessage.innerText = '';
	    gameOver = false;


	    //popup.style.display = 'none';
	});










}









