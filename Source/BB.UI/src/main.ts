import { createApp } from 'vue';
import { Quasar } from 'quasar'
import { createPinia } from 'pinia';

// Import icon libraries
import '@quasar/extras/material-icons/material-icons.css'
// Import Quasar css
import 'quasar/src/css/index.sass'

import App from './App.vue';
import router from './router';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(Quasar, {
    config: {
        brand: {
            primary: '#464655',
            secondary: '#f3d9dc',
            accent: '#5d8298',

            dark: '#1c1c22',

            positive: '#7b9e89',
            negative: '#d95d39',
            info: '#336699',
            warning: '#fed766'
        }
    }
});

app.mount('#app');
