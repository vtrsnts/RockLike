
import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Route, Redirect } from 'react-router-dom'

export class RockLike extends Component {

    constructor(props) {
        super(props);
        this.state = { likes: 0, loading: true };
        this.addLike = this.addLike.bind(this);
    }
    componentDidMount() {
        this.populateLikesCount();
    }

    async addLike() {
        let token = await authService.getAccessToken();
        let url = "SiteLikes/PostSiteLike";
        let urlatual = window.location.href;
        let site = {
            Url: urlatual
        }
        const requestOptions = {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-type': 'application/json'
            },
            body: JSON.stringify(site)
        };
        debugger;
        const response = await fetch(url, requestOptions)
        if (response.ok) {
            const data = await response.json();
            this.setState({ likes: data, loading: false });
        }
        else if ([401].indexOf(response.status) !== -1) {
            this.setState({ loading: false, statusRequest:401 });
        }
    };

    refreshPage() {
        this.populateLikesCount();
    }

    renderButtonLikeCount() {
        var spanCurtidas;
        var erro;
        let likeCount = this.state.likes;
        if (likeCount === 0)
            spanCurtidas = "Seja o primeiro a curtir!"
        else
            spanCurtidas = likeCount === 1 ? `${likeCount} Curtida` : `${likeCount} Curtidas`;
        if (this.state.statusRequest == 401)
            erro = "Favor realizar login para Curtir!"

        return (

            <div style={{ borderTop: "2px solid #fff " }} >
                <button className="button" onClick={this.addLike}>
                    Curtir
                 </button>
                <span style={{ marginLeft: 20, marginRight: 20 }} >{spanCurtidas}</span>
                <span style={{ marginLeft: 20, marginRight: 20, color: 'red' }} >{erro}</span>
            </div>
        );
    }




    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderButtonLikeCount();

        return (contents);

    }

    async populateLikesCount() {


        let url = `SiteLikes/GetSiteLikeCount?url=${window.location.href}`;
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ likes: data, loading: false });

    }
}

