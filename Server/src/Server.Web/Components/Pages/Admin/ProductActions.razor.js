const photoUpload = document.getElementById("photoFile");
const photoUrl = document.getElementById("photoUrl");
const requestVerificationField = 
    document.getElementsByName("RequestVerificationTokenField").item(0);

export function onLoad() {
    photoUpload.addEventListener("change", uploadImage);
}

export function onUpdate() {
    photoUpload.addEventListener("change", uploadImage);
}

export function onDispose() {
    photoUpload.removeEventListener("change", uploadImage);
}

async function uploadImage() {
    const formData = new FormData();
    formData.append("imageFile", photoUpload.files[0]);

    fetch("/api/images/upload", {
        method: "POST",
        headers: {
            "Accept": "*/*",
            "X-CSRF-TOKEN": requestVerificationField.value
        },
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            photoUrl.value = data.imageUrl;
        })
        .catch(error => console.log(error));
}