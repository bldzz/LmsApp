/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{cshtml,razor}",
        "./Views/Shared/**/*.cshtml",
        "./Pages/**/*.{cshtml,razor}",
        "./wwwroot/js/**/*.js"],
    theme: {
        extend: {},
    },
    plugins: [],
}