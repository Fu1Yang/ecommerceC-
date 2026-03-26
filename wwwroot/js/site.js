

const image = document.querySelector("#image")
const array = ['/images/BMW-M2.jpg', '/images/bmwI8.jpg', '/images/bmw-smg.jpeg', '/images/e46.jpg', '/images/gts.jpg']
const menuBar = document.querySelector(".menu-bar")

let index = 0;
    let valeur = "";
    image.style.width = "80px";
    image.style.height = "40px";
    image.style.borderRadius = "40px";

    const carrousel = setInterval(() => {
        image.src = array[index];
        index++
        if (index > array.length - 1) index = 0;
    }, 1000) 


function showAlert() {
    valeur = prompt("Tapez votre id : ")
    console.log("id " + valeur);
}

function hideMenu() {
        menuBar.style.display = "none ";
    }
function displayMenu() {
        menuBar.style.display = "flex";
    }

    /** 
     * LE CONTENUE DU CODE DE LA PLUIE
     */

    var nbDrop = 858;


    function randRange(minNum, maxNum) {
        return (Math.floor(Math.random() * (maxNum - minNum + 1)) + minNum);
    }


    function createRain() {

        for (i = 1; i < nbDrop; i++) {
            var dropLeft = randRange(0, window.innerWidth);
            var dropTop = randRange(-window.innerHeight, window.innerHeight);

            $('.rain').append('<div class="drop" id="drop' + i + '"></div>');
            $('#drop' + i).css('left', dropLeft);
            $('#drop' + i).css('top', dropTop);
        }

    }
    // Make it rain
    createRain();

