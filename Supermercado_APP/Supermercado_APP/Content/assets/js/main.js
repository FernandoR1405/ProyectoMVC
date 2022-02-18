
function imagechange(id){
    let imageUrl = 'Content/assets/img/'+ id +'.jpg';
    $('#row').css('background-image', 'url(' + imageUrl + ')');
    console.log(imageUrl);
};



let frutas = [1, 2, 3, 4, 5]
for (let i = 0; i < frutas.length; i++) 
{
    let imagerl = 'Content/assets/img/'+ frutas[i] +'.jpg';
    document.getElementById(frutas[i]).style.backgroundImage = "url(" + imagerl +")";
}
