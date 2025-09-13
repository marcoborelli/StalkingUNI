import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/

export default defineConfig(() => {
  const env = loadEnv('', './', '')
  return {
    plugins: [react()],
    base: env.VITE_PUBLIC_URL_BASE_PATH,
    server: {
      host: '0.0.0.0',
      port: parseInt(env.VITE_PORT)
    }
  }
})