module.exports = {
  purge: {
    enabled: true,
    content: [
      './Pages/**/*.cshtml',
      './Views/**/*.cshtml',
    ],
  },
  darkMode: 'class', // or 'media' or 'class'
  theme: {
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [
    require('flowbite/plugin')
  ],
  content: [
    "./node_modules/flowbite/**/*.js"
  ]
}
