import React from "react";
import { PostPreviewDto, PostsApi } from "../../api";
import PostList from "../../components/posts/postList";
import { Row, Col } from "antd";

interface IPosts {
  posts: PostPreviewDto[];
}

const Index = ({ posts }: IPosts) => {
  return (
    <Row>
      <Col span={16}>
        <PostList posts={posts} />
      </Col>
    </Row>
  );
};

Index.getInitialProps = async () => {
  const api = new PostsApi();
  try {
    const request = await api.postsGetAll();
    return { posts: await request.data };
  } catch (e) {
    console.log(e);
  }
};

export default Index;
