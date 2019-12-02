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

// браузер поддерживает уведомления
// вообще, эту проверку должна делать библиотека Firebase, но она этого не делает
if ('Notification' in window) {
  var messaging = firebase.messaging();

  // пользователь уже разрешил получение уведомлений
  // подписываем на уведомления если ещё не подписали
  subscribe();

  messaging.onMessage(function (payload) {
    console.log('Message received. ', payload);

    // регистрируем пустой ServiceWorker каждый раз
    navigator.serviceWorker.register('messaging-sw.js');

    // запрашиваем права на показ уведомлений если еще не получили их
    Notification.requestPermission(function (result) {
      if (result === 'granted') {
        navigator.serviceWorker.ready.then(function (registration) {
          // теперь мы можем показать уведомление
          return registration.showNotification(payload.notification.title, payload.notification);
        }).catch(function (error) {
          console.warn('ServiceWorker registration failed', error);
        });
      }
    });
  });

  // subscribe();
}

function subscribe() {
  // запрашиваем разрешение на получение уведомлений
  messaging.requestPermission()
    .then(function () {
      // получаем ID устройства
      messaging.getToken()
        .then(function (currentToken) {
          if (currentToken) {
            sendTokenToServer(currentToken);
          } else {
            console.warn('Не удалось получить токен.');
            setTokenSentToServer(false);
          }
        })
        .catch(function (err) {
          console.warn('При получении токена произошла ошибка.', err);
          setTokenSentToServer(false);
        });
    })
    .catch(function (err) {
      console.warn('Не удалось получить разрешение на показ уведомлений.', err);
    });
}

// отправка ID на сервер
function sendTokenToServer(currentToken) {
  if (!isTokenSentToServer(currentToken)) {
    var url = '/Authentication/AddBrowserId'; // адрес скрипта на сервере который сохраняет ID устройства
    $.post(url, {
      token: currentToken
    }, () => setTokenSentToServer(currentToken));
  }
}

// используем localStorage для отметки того,
// что пользователь уже подписался на уведомления
function isTokenSentToServer(currentToken) {
  return window.localStorage.getItem('sentFirebaseMessagingToken') == currentToken;
}

function setTokenSentToServer(currentToken) {
  window.localStorage.setItem(
    'sentFirebaseMessagingToken',
    currentToken ? currentToken : ''
  );
}