module.exports = {
    "/weather-api": {
      target:
        process.env["services__weatherapi__https__0"] ||
        process.env["services__weatherapi__http__0"],
      secure: process.env["NODE_ENV"] !== "development",
      pathRewrite: {
        "^/weather-api": "",
      },
    },
    "/production-gateway": {
      target:
        process.env["services__production-gateway__https__0"] ||
        process.env["services__production-gateway__http__0"],
      secure: process.env["NODE_ENV"] !== "development",
      pathRewrite: {
        "^/production-gateway": "",
      },
    },
  };