function AddCart(produitId) {
    console.log("=== Ajout au panier ===");
    console.log("Produit ID:", produitId);

    const formData = new FormData();
    formData.append('productId', produitId);
    // / donner le choix de la quantité à mettre dans le panier a faire plus tard.

    // Utilisez une URL relative sans spécifier le domaine
    fetch('?handler=AddToCart', {
        method: 'POST',
        body: formData,
        headers: {
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        }
    })
        .then(response => {
            console.log("Status:", response.status);
            if (response.ok) {
                return response.json();
            } else {
                throw new Error(`HTTP ${response.status}`);
            }
        })
        .then(data => {
            console.log("Réponse:", data);
            if (data.success) {
                alert('Produit ajouté au panier !');
            }
        })
        .catch(error => {
            console.error("Erreur:", error);
            alert(`Erreur: ${error.message}`);
        });
}