import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    watch: {
      usePolling: true,
    },
    host: true,
    strictPort: true,
    port: 80,
    hmr: {
      port: 34,
      clientPort: 34,
    },
  },
  build: {
    outDir: 'build',
    sourcemap: false,
  },
})
