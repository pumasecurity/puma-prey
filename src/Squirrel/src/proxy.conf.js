const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "https://localhost:500",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
