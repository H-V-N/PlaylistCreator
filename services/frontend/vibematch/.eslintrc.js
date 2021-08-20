module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: [
    'plugin:vue/recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:prettier/recommended'
  ],
  //extends: ["prettier", "prettier/standard", "plugin:@typescript-eslint/recommended", "plugin:vue/recommended"],
  plugins: ["vue", "prettier"],
  parser: "vue-eslint-parser", 
  parserOptions: {
    parser: '@typescript-eslint/parser',
    sourceType: 'module',
    ecmaVersion: 2020
  },
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'vue/component-name-in-template-casing': ['error', 'PascalCase'],
    '@typescript-eslint/explicit-module-boundary-types': 'off'
  }
};

