import { Divider, Typography } from "antd";
import React from "react";
import { PostPreviewDto } from "../../api";
import Card from "../common/card";

const { Title, Paragraph } = Typography;

interface IPost {
  post: PostPreviewDto;
}

const Post = ({ post }: IPost) => {
  return (
    <Card>
      <Typography>
        <Title level={3}>{post.title}</Title>
        <Divider />
        <Paragraph>{post.excerpt}</Paragraph>
      </Typography>
    </Card>
  );
};

export default Post;
