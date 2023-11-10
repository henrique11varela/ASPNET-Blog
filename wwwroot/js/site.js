// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
localStorage.setItem("theme", "")
const button = document.getElementById("themeToggler")
button.addEventListener("click", () => {
    if (localStorage.getItem("theme") != "dark") {
        document.documentElement.classList.add("dark");
        localStorage.setItem("theme", "dark")
    } else {
        document.documentElement.classList.remove("dark");
        localStorage.setItem("theme", "")
    }
})