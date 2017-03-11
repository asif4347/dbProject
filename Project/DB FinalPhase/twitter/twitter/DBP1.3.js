var imageID = 0;
function changebg(every_seconds) {
    if (!imageID) {
        document.body.style.backgroundImage = 'url(Images/' + 'bg2' + '.jpg)';
        imageID++;
    }
    else {
        if (imageID == 1) {
            document.body.style.backgroundImage = 'url(Images/' + 'bg3' + '.jpg)';
            imageID++;
        }
        else if (imageID == 2) {
            document.body.style.backgroundImage = 'url(Images/' + 'bg2' + '.jpg)';
            imageID++;
        }
        else if (imageID == 3) {
            document.body.style.backgroundImage = 'url(Images/' + 'bg1' + '.jpg)';
            imageID++;
        }
        else if (imageID == 4) {
            document.body.style.backgroundImage = 'url(Images/' + 'bg4' + '.jpg)';
            imageID = 1;
        }

    }
    setTimeout("changebg(" + every_seconds + ")", ((every_seconds) * 1000));
}

function imup() {
    document.getElementById("FileUpload1").click();
}

function reset() {
    changebg(5);
    document.getElementById("signin").value = '';
    document.getElementById("pass").value = '';
    document.getElementById("sun").value = '';
    document.getElementById("sue").value = '';
    document.getElementById("sup").value = '';
    document.getElementById("supass").value = '';
}

function login() {

    if (checkemail() == false) {
        document.getElementById("Message").style.display = "block";
    }
    else {

    }
}

function checkemail() {
    var em = document.getElementById("signin").value;
    var em_size = em.length;
    if (em_size == 0) {
        alert("Invalid Email or Phone!");
        return false;
    }
    else
        return true;
}

function rel() {
    document.getElementById("DP").setAttribute('src', document.getElementById("imgurl").value);
    document.getElementById("cti").setAttribute('src', document.getElementById("imgurl").value);
}