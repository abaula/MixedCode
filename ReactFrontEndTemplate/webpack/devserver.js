module.exports = function () {
    return {
        devServer: {
            stats: 'errors-only',
            port: 9000,
            historyApiFallback: true
        }
    }
};



