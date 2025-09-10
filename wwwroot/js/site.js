﻿const image = document.querySelector("#image")
const array = ['/images/BMW-M2.jpg', '/images/bmwI8.jpg', '/images/bmw-smg.jpeg', '/images/e46.jpg', '/images/gts.jpg']
const menuBar = document.querySelector(".menu-bar")
let index = 0;
let valeur = "";
image.style.width = "500px";
image.style.height = "300px";

const carrousel = setInterval(() => {
    image.src =  array[index];
    index++
    if (index > array.length - 1) index = 0;
}, 5000) 


function showAlert() {
    valeur = prompt("Tapez votre id : ")
    console.log("id "+valeur);
}

function hideMenu() {
    menuBar.style.display = "none ";
}
function dispayMenu() {
    menuBar.style.display = "flex";
}