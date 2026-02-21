/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: 'class',
    content: [
        './Pages/**/*.{razor,html}',
        './Shared/**/*.{razor,html}',
        './wwwroot/**/*.html',
        './App.razor',
        './_Imports.razor'
    ],
    theme: {
        extend: {
            colors: {
                border: "hsl(var(--border))",
                input: "hsl(var(--input))",
                ring: "hsl(var(--ring))",
                background: "hsl(var(--background))",
                foreground: "hsl(var(--foreground))",
                primary: {
                    DEFAULT: "hsl(var(--primary))",
                    foreground: "hsl(var(--primary-foreground))",
                },
                secondary: {
                    DEFAULT: "hsl(var(--secondary))",
                    foreground: "hsl(var(--secondary-foreground))",
                },
                destructive: {
                    DEFAULT: "hsl(var(--destructive))",
                    foreground: "hsl(var(--destructive-foreground))",
                },
                muted: {
                    DEFAULT: "hsl(var(--muted))",
                    foreground: "hsl(var(--muted-foreground))",
                },
                accent: {
                    DEFAULT: "hsl(var(--accent))",
                    foreground: "hsl(var(--accent-foreground))",
                },
                popover: {
                    DEFAULT: "hsl(var(--popover))",
                    foreground: "hsl(var(--popover-foreground))",
                },
                card: {
                    DEFAULT: "hsl(var(--card))",
                    foreground: "hsl(var(--card-foreground))",
                },
                'premium-orange': {
                    50: '#fff7ed',
                    100: '#ffedd5',
                    200: '#fed7aa',
                    300: '#fdba74',
                    400: '#fb923c',
                    500: '#f97316', // Primary Brand Color
                    600: '#ea580c',
                    700: '#c2410c',
                    800: '#9a3412',
                    900: '#7c2d12',
                },
                'dark-panel': '#1a1a1a',
                'dark-sidebar': '#111111',
            },
            borderRadius: {
                lg: "var(--radius)",
                md: "calc(var(--radius) - 2px)",
                sm: "calc(var(--radius) - 4px)",
            },
            fontFamily: {
                'sans': ['Inter', 'ui-sans-serif', 'system-ui'],
                'outfit': ['Outfit', 'sans-serif'],
            },
            boxShadow: {
                'premium-1': '0 2px 8px rgba(0, 0, 0, 0.04)',
                'premium-2': '0 4px 16px rgba(0, 0, 0, 0.06)',
                'premium-3': '0 8px 24px rgba(0, 0, 0, 0.08)',
                'premium-4': '0 12px 40px rgba(0, 0, 0, 0.12)',
                'premium-orange': '0 8px 24px rgba(249, 115, 22, 0.25)',
                'premium-orange-lg': '0 12px 32px rgba(249, 115, 22, 0.3)',
                'glow-orange': '0 0 20px rgba(249, 115, 22, 0.15)',
                'inner-premium': 'inset 0 1px 0 rgba(255, 255, 255, 0.6)',
            },
            spacing: {
                '18': '4.5rem',
                '88': '22rem',
                '128': '32rem',
            },
            animation: {
                'fade-in': 'fadeIn 0.5s ease-out',
                'slide-up': 'slideUp 0.5s ease-out',
                'scale-in': 'scaleIn 0.3s ease-out',
                'ripple': 'ripple 0.6s linear',
            },
            keyframes: {
                fadeIn: {
                    '0%': { opacity: '0' },
                    '100%': { opacity: '1' },
                },
                slideUp: {
                    '0%': { transform: 'translateY(10px)', opacity: '0' },
                    '100%': { transform: 'translateY(0)', opacity: '1' },
                },
                scaleIn: {
                    '0%': { transform: 'scale(0.95)', opacity: '0' },
                    '100%': { transform: 'scale(1)', opacity: '1' },
                },
                ripple: {
                    '0%': { transform: 'scale(0)', opacity: '1' },
                    '100%': { transform: 'scale(4)', opacity: '0' },
                },
            },
            backdropBlur: {
                'xs': '2px',
                'premium': '20px',
            },
            backdropSaturate: {
                '180': '1.8',
            },
        },
    },
    plugins: [],
}
