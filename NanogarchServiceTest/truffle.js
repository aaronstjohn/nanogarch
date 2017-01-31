module.exports = {
  build: {
    "index.html": "index.html",
    "app.js": [
      "javascripts/app.js"
    ],
    "app.css": [
      "stylesheets/app.css"
    ],
    "images/": "images/",
    "Release/":"unity/Release/",
    "TemplateData/":"unity/TemplateData/"
  },
  rpc: {
    host: "localhost",
    port: 8545
  }
};
