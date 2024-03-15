function Loguear(){
    var matricula = document.getElementById("Matricula");
    var clave = document.getElementById("Clave");

    window.location.href="principal.html";

}
function CambiarPantalla(Pantalla){

    switch(Pantalla){
        case 1: window.location.href="Pantallas/principal.html";break;
        case 2: window.location.href="Pantallas/principal2.html";break;
        case 3: window.location.href="Pantallas/principal3.html";break;
        case 4: window.location.href="Pantallas/principal4.html";break;
        case 5: window.location.href="Pantallas/principal5.html";break;
        default: window.location.href="principa.html";
    }

}