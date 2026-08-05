const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "http://localhost:8000",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
