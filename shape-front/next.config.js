/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
    swcMinify: true,
    webpack: config => {
        config.watchOptions = {
            poll: 1000,
            aggregateTimeout: 300,
        };
        return config;
    },
    rewrites: proxy => {
        proxy = [
            {
                source: "/shapeAPI/:path*",
                destination: "http://shape-api:80/:path*"
            }
        ];

        return proxy;
    }

}

module.exports = nextConfig
