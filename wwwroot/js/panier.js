
function AddCart(produitId) {
    console.log(produitId)
    fetch('/PiecesAuto?handler=Add', {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ productId: produitId })
    }).then(response => {
        if (response.ok) alert('Produit ajoute au panier')
    }); 
}