// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const button = document.getElementById("theme-toggle")
const themeToggleDarkIcon = document.getElementById('theme-toggle-dark-icon');
const themeToggleLightIcon = document.getElementById('theme-toggle-light-icon');
button.addEventListener("click", () => {
    if (localStorage.getItem("theme") != "dark") {
        document.documentElement.classList.add("dark");
        themeToggleDarkIcon.classList.add('hidden');
        themeToggleLightIcon.classList.remove('hidden');
        localStorage.setItem("theme", "dark")
    } else {
        document.documentElement.classList.remove("dark");
        themeToggleDarkIcon.classList.remove('hidden');
        themeToggleLightIcon.classList.add('hidden');
        localStorage.setItem("theme", "")
    }
})
if (localStorage.getItem("theme") != "dark") {
    document.documentElement.classList.remove("dark");
    themeToggleDarkIcon.classList.remove('hidden');
    themeToggleLightIcon.classList.add('hidden');
} else {
    document.documentElement.classList.add("dark");
    themeToggleDarkIcon.classList.add('hidden');
    themeToggleLightIcon.classList.remove('hidden');
}
