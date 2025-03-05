module.exports = {
    "/weather-api": {
      target:
        process.env["services__weatherApi__https__0"] ||
        process.env["services__weatherApi__http__0"],
      secure: process.env["NODE_ENV"] !== "development",
      pathRewrite: {
        "^/weather-api": "",
      },
    },

    "/productionGateway": {
      target:
        process.env["services__productionGateway__https__0"] ||
        process.env["services__productionGateway__http__0"],
      secure: process.env["NODE_ENV"] !== "development",
      pathRewrite: {
        "^/productionGateway": "",
      },
    },

    "/stagingGateway": {
      target:
        process.env["services__stagingGateway__https__0"] ||
        process.env["services__stagingGateway__http__0"],
      secure: process.env["NODE_ENV"] !== "development",
      pathRewrite: {
        "^/stagingGateway": "",
      },
    },
  };