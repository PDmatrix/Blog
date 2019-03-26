import "antd/dist/antd.css";
import "../static/app.css";
import App, { Container } from "next/app";
import Head from "next/head";
import React from "react";
import Layout from "../components/layout/layout";

export default class MyApp extends App {
  public render() {
    const { Component, pageProps } = this.props;
    return (
      <Container>
        <Head>
          <meta name="viewport" content="width=device-width, initial-scale=1" />
          <title>Blog</title>
        </Head>
        <Layout>
          <Component {...pageProps} />
        </Layout>
      </Container>
    );
  }
}
