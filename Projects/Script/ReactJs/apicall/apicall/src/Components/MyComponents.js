import React, { Component } from 'react';
<script crossorigin src="..."></script>

class MyComponents extends Component {

    constructor(props){
        super(props);
        this.state = {
            error: null,
            items: [],
            isLoaded: false
        };
    }

    componentDidMount() {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Accept', 'application/json');
        headers.append('Access-Control-Allow-Origin', '*');

        fetch("http://localhost:55556/readings/read/smart-meter-1",{mode: 'cors',headers: headers, method: 'GET'})
        .then(res => res.json())
        .then(
        (result)=> {
            console.log(result);

            this.setState = ({
                isLoaded: true,
                items: result.items
            });
        },
        (error)=>{
            console.log(error);

          this.setState = ({
              isLoaded:false,
              error
          });
        }
        )
    }

    render() {
        const {error, isLoaded, items}=this.state;
        if(error){
           return <div>Error: { error.message }</div>
        }
        else if(!isLoaded){
            return <div>Loading....!</div>
        }
        else{
            return (
               <ul>
                   {
                       items.map(item => (
                         <li>
                             hi
                        </li>
                       ))
                   }
               </ul>
            )
        }
    }
}

export default MyComponents;
