// Utilitaires
}));


const subtotal = rows.reduce((s, r) => s + r.total, 0);
const discount = Number(document.getElementById('discount').value) || 0;
const vat = Number(document.getElementById('vat').value) || 0;
const taxable = Math.max(0, subtotal - discount);
const tvaAmount = taxable * (vat / 100);
const grand = taxable + tvaAmount;


return {
    company: document.getElementById('company').value,
    contact: document.getElementById('contact').value,
    email: document.getElementById('email').value,
    phone: document.getElementById('phone').value,
    address: document.getElementById('address').value,
    date: document.getElementById('date').value,
    project: document.getElementById('project').value,
    payment: document.getElementById('payment').value,
    notes: document.getElementById('notes').value,
    items: rows,
    subtotal, discount, vat, tvaAmount, grand
};
}


function renderPrintable(d) {
    // Minimal printable template (can be improved)
    const rowsHtml = d.items.map(it => `
<tr>
<td>${escapeHtml(it.desc)}</td>
<td style="text-align:right">${it.qty}</td>
<td style="text-align:right">${it.price.toFixed(2)} €</td>
<td style="text-align:right">${it.total.toFixed(2)} €</td>
</tr>
`).join('');


    return `<!doctype html><html lang="fr"><head><meta charset="utf-8"><title>Devis - ${escapeHtml(d.company)}</title>
<style>body{font-family:Arial,Helvetica,sans-serif;padding:24px;color:#111}table{width:100%;border-collapse:collapse}th,td{padding:8px;border-bottom:1px solid #ddd}h2{margin:0 0 10px}</style>
</head><body>
<h2>Devis</h2>
<div><strong>Client :</strong> ${escapeHtml(d.company)} — ${escapeHtml(d.contact)}</div>
<div><strong>Email :</strong> ${escapeHtml(d.email)} — <strong>Tél :</strong> ${escapeHtml(d.phone)}</div>
<div style="margin-top:8px"><strong>Date :</strong> ${escapeHtml(d.date)}</div>
<h3 style="margin-top:18px">Description</h3>
<div>${escapeHtml(d.project).replace(/\n/g, '<br/>')}</div>
<h3 style="margin-top:12px">Prestations</h3>
<table><thead><tr><th>Désignation</th><th style="text-align:right">Quantité</th><th style="text-align:right">PU</th><th style="text-align:right">Total</th></tr></thead><tbody>
${rowsHtml}
</tbody></table>
<div style="margin-top:12px;text-align:right">
<div>Sous-total : ${d.subtotal.toFixed(2)} €</div>
<div>Remise : ${d.discount.toFixed(2)} €</div>
<div>TVA (${d.vat}%) : ${d.tvaAmount.toFixed(2)} €</div>
<div style="font-weight:700;margin-top:6px">TOTAL TTC : ${d.grand.toFixed(2)} €</div>
</div>
<h4 style="margin-top:18px">Conditions</h4>
<div>${escapeHtml(d.payment).replace(/\n/g, '<br/>')}</div>
<div style="margin-top:6px;color:#666">${escapeHtml(d.notes).replace(/\n/g, '<br/>')}</div>
<script>window.print()</script>
</body></html>`;
}


function printQuote() {
    const data = gatherData();
    const html = renderPrintable(data);
    const w = window.open('about:blank', '_blank');
    w.document.write(html);
    w.document.close();
}


// Escape helper
function escapeHtml(s) {
    if (!s) return '';
    return String(s).replace(/[&<>"'`]/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;', "`": '&#96;' })[c]);
}


// initial row
addItem({ desc: '', qty: 1, price: 0 });