importScripts('https://www.gstatic.com/firebasejs/3.6.8/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/3.6.8/firebase-messaging.js');

var firebaseConfig = {
    apiKey: "AIzaSyBtsbJsgq5g6cBO2TE6JOVPllkE6wA8ilg",
    authDomain: "calendarpush-74bfd.firebaseapp.com",
    databaseURL: "https://calendarpush-74bfd.firebaseio.com",
    projectId: "calendarpush-74bfd",
    storageBucket: "calendarpush-74bfd.appspot.com",
    messagingSenderId: "1057839628645",
    appId: "1:1057839628645:web:0f11859e8c0641b749a18c"
};
firebase.initializeApp(firebaseConfig);

//firebase.initializeApp({
//    messagingSenderId: 'AAAA9kwnwWU:APA91bFBbO0RuT8DuXExUKWT1ZK90wIVjqu-clS2zn9iC6N-HG71ktbv4yleOqZa10EBsB8ynxLEyxD01k60T5ttOoCucd7KwAJ9r7u3G6jSLsR4W4XN2fwchBHEwP9317rWEbYLkwU-'
//});

const messaging = firebase.messaging();