import '../styles/globals.css'
import type { AppProps } from 'next/app'
import { ThemeProvider } from '@material-tailwind/react';

function MyApp({ Component, pageProps }: AppProps) {
    return <div className="select-none">
        <ThemeProvider>
            <Component {...pageProps} />
        </ThemeProvider>
    </div>;
}

export default MyApp
