module.exports = {
    "/envGateway": {
      target:
        process.env["services__ProductionGateway__https__0"] ||
        process.env["services__ProductionGateway__http__0"],
      secure: process.env["NODE_ENV"] !== "development",
      pathRewrite: {
        "^/envGateway": "",
      },
    },
  };