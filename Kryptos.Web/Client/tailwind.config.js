const colors = require('tailwindcss/colors');

module.exports = {
    darkMode: 'class',
    content: [
        './**/*.html',
        './**/*.razor'
    ],
    theme: {
        colors: {
            transparent: 'transparent',
            current: 'currentColor',
            black: colors.black,
            white: colors.white,
            rose: colors.rose,
            pink: colors.pink,
            fuchsia: colors.fuchsia,
            purple: colors.purple,
            violet: colors.violet,
            indigo: colors.indigo,
            blue: colors.blue,
            sky: colors.sky,
            cyan: colors.cyan,
            teal: colors.teal,
            emerald: colors.emerald,
            green: colors.green,
            lime: colors.lime,
            yellow: colors.yellow,
            amber: colors.amber,
            orange: colors.orange,
            red: colors.red,
            stone: colors.stone,
            neutral: colors.neutral,
            gray: colors.gray,
            slate: colors.slate,
        },
        extend: {
            height: {
                '9/10' : '90%',
            },
            keyframes: {
                'fade-in': {
                    '0%': {
                        opacity: '0',
                    },
                    '100%': {
                        opacity: '1',
                    },
                }
            },
            animation: {
                'fade-in': 'fade-in 1.5s ease-out'
            }
        },
    },
    plugins: [
        require('@tailwindcss/typography'),
    ],
}
