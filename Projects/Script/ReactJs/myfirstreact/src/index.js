import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
//import App from './App';
import Garage from './Car';
import reportWebVitals from './reportWebVitals';

// ReactDOM.render(
//   <React.StrictMode>
//     <App />
//   </React.StrictMode>,
//   document.getElementById('root')
// );

//const myfirstElement = <h1>Senbaga Kumar !</h1>
ReactDOM.render(
 // <Car/>, document.getElementById('text2') // Class Componenet sample
 //<Car color='red' />, document.getElementById('text2') // Props sample
 <Garage />, document.getElementById('text2')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
