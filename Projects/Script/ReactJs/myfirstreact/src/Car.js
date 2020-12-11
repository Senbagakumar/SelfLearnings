import React from 'react';

class Car extends React.Component{
    constructor(){
        super();
        //this.state = { color: 'red'};
    }
    render() {
       // return <h2>Hi, I am a { this.state.color } Car !</h2>
        return <h2>Hi, I am a { this.props.Brand.color } Car and Nice Brand !</h2>
    }
}


export default class Garage extends React.Component {
    render(){
        //const brand="Ford";
        const carinfo = { Brand: "Ford", color: "Red"};
        return (
            <div>
               <h1> Who lives in my Garage ? </h1>
            {/* <Car Brand={brand} /> */}
             <Car Brand={carinfo} />
            </div>
        );
    }
}