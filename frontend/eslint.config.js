import js from '@eslint/js'
import pluginVue from 'eslint-plugin-vue'

export default [
  js.configs.recommended,
  ...pluginVue.configs['flat/recommended'],
  {
    files: ['**/*.{js,vue}'],
    languageOptions: {
      ecmaVersion: 'latest',
      sourceType: 'module',
      globals: {
        console: 'readonly',
        fetch: 'readonly',
        window: 'readonly',
        document: 'readonly'
      }
    },
    rules: {
  'vue/multi-word-component-names': 'off',
  'no-unused-vars': 'warn',
  'vue/no-unused-vars': 'warn',
  'vue/html-self-closing': 'off',
  'vue/html-closing-bracket-newline': 'off',
  'vue/html-indent': 'off',
  'vue/html-closing-bracket-spacing': 'off',
  'vue/max-attributes-per-line': 'off',
  'vue/singleline-html-element-content-newline': 'off',
  'vue/multiline-html-element-content-newline': 'off',
  'vue/attributes-order': 'off',
  'vue/first-attribute-linebreak': 'off'
}
  }
]
