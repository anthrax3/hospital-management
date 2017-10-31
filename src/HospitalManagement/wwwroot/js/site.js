// Write your Javascript code.
// Write your Javascript code.
function checkEmail() {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var email = document.getElementById("Email").value;

    if (!re.test(email)) {
        document.getElementById("emailCheckSpan").innerHTML = "Invalid Email Address";

    }
    else {
        document.getElementById("emailCheckSpan").innerHTML = "";

    }

}

function loginBtn() {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var email = document.getElementById("Email").value;


    if (document.getElementById("LoginPassword").value == "" || email == "" || !re.test(email)) {
        document.getElementById("loginCheckSpan").innerHTML = "Fill the fields properly";
    }
    else {

        document.getElementById("loginCheckSpan").innerHTML = "";
    }


}

function checkAlphabets(event) {
    var x = event.which || event.keyCode;


    if ((x >= 65 && x <= 90) || (x >= 97 && x <= 122)) {
        return true;

    }
    else
        return false;


}


function checkNumbers(event) {
    var x = event.which || event.keyCode;


    if (x >= 48 && x <= 57) {
        return true;

    }
    else
        return false;


}


function createAccBtn() {

    var pass = document.getElementById("LoginPassword").value;
    var contact = document.getElementById("ContactNo").value;

    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var email = document.getElementById("Email").value;


    if (!re.test(email) || document.getElementById("Fname").value == "" || document.getElementById("Lname").value == "" || document.getElementById("Email").value == "" || document.getElementById("LoginPassword").value == "" || document.getElementById("LoginPasswordConf").value == "" || document.getElementById("bday").value == "" || document.getElementById("country").value == "" || document.getElementById("city").value == "" || document.getElementById("ContactNo").value == "" || document.getElementById("Address").value == "") {
        document.getElementById("SignUpCheckSpan").innerHTML = "Fill the fields properly *";
        return;
    }

    else if (pass.length < 6) {
        document.getElementById("SignUpCheckSpan").innerHTML = "Password cannot be less than 6 letters";
        return;


    }
    else if (document.getElementById("LoginPassword").value != document.getElementById("LoginPasswordConf").value) {

        document.getElementById("SignUpCheckSpan").innerHTML = "Passwords donot match";
        return;
    }

    else if (contact.length < 11) {
        document.getElementById("SignUpCheckSpan").innerHTML = "Contact Number is invalid";
        return;


    }


    else {

        document.getElementById("SignUpCheckSpan").innerHTML = "";

        document.getElementById("SignUpForm").submit();


    }




}

function AddPatientBtn() {

    var contact = document.getElementById("ContactNo").value;

    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var email = document.getElementById("Email").value;


    if (!re.test(email) || document.getElementById("Fname").value == "" || document.getElementById("Lname").value == "" || document.getElementById("Email").value == "") {
        document.getElementById("AddPatientSpan").innerHTML = "Fill the fields properly *";
        return;
    }


    else if (contact.length < 11 && contact.length > 0) {
        document.getElementById("AddPatientSpan").innerHTML = "Contact Number is invalid";
        return;


    }
    else if (document.getElementById("bG").value == "select") {
        document.getElementById("AddPatientSpan").innerHTML = "Select a valid blood group";
        return;

    }




    else {

        document.getElementById("AddPatientSpan").innerHTML = "";

        document.getElementById("AddPatientForm").submit();


    }



}