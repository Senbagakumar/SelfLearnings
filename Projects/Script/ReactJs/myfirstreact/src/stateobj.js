import React from 'react';

class NewCar extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            brand: "BMW",
            model: "Mustang",
            color: "Red",
            year : "2020"
        };
    }

    changeColor = () => { this.setState({color: "blue"})};

    render() {
        return(
            <div>
                <h1>My {this.state.brand } </h1>
                <p>
                    It is a {this.state.color} color And {this.state.model} model from {this.state.year} year.
                </p>
                <button type="button" onClick = {this.changeColor}> ChangeColor </button>
            </div>
        );
    }

}

export default NewCar;