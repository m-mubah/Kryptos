module.exports = {
    darkMode: 'class',
    content: [
        './**/*.html',
        './**/*.razor'
    ],
    theme: {
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
    plugins: [],
}
