// Acceso a localStorage desde Blazor.
//
// Se envuelve cada operación en try/catch porque localStorage no es un simple
// diccionario: lanza excepción si el navegador tiene bloqueado el
// almacenamiento del sitio (modo incógnito, configuración de privacidad) o si
// se superó la cuota. Sin este catch la excepción cruzaría a C# y cortaría el
// circuito de Blazor, tirando abajo la página entera.
//
// Con el catch, el peor caso es que el carrito no se persista: sigue
// funcionando en memoria durante la sesión.

window.almacenamiento = {

    // Devuelve true si se pudo guardar, false si el navegador lo impidió.
    guardar: function (clave, valor) {
        try {
            localStorage.setItem(clave, valor);
            return true;
        } catch (error) {
            console.warn('No se pudo guardar en localStorage:', error);
            return false;
        }
    },

    // Devuelve el valor guardado, o null si no existe o no se pudo leer.
    leer: function (clave) {
        try {
            return localStorage.getItem(clave);
        } catch (error) {
            console.warn('No se pudo leer de localStorage:', error);
            return null;
        }
    },

    borrar: function (clave) {
        try {
            localStorage.removeItem(clave);
        } catch (error) {
            console.warn('No se pudo borrar de localStorage:', error);
        }
    }
};
