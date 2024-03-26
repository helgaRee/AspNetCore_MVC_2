document.addEventListener("DOMContentLoaded", function () {
    let switchMode = document.querySelector('#switch-mode') //hämtar elementets id ur DOM-trädet och lagrar

    switchMode.addEventListener('change', function () {//lägger till en e-listener på swtitchmode-elementet
        let theme = this.checked ? "dark" : "light" //kontroll om  checked lr ej

        //kör action på controllern genom js, utan att sidan laddar om sig - kommer åt endpoints i controllern
        //fetch anropas för att göra en HTTP-begäran
        fetch(`/sitesettings/changetheme?mode=${theme}`) //theme = variabeln som ska skickas med
            .then(res => { //öppnar upp res
                if (res.ok) //om res är OK (se SiteSettingsConttroller)
                    window.location.reload() //omrendera fönstret - uppdaterar sig
                else
                console.log('something')
            })
    })
})