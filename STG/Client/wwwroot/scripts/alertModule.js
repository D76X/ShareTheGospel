
// the export keyword remove the function from the Global Scope
// and create a JS module whose name is the name of the file
// that contains the export statement.
// Any functions without teh EXPORT are private to the module.
export function showAlert(message) {
    alert(message);
};

