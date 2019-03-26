import React from "react";
import { PostPreviewDto } from "../../api";
import Post from "./post";

interface IPostList {
  posts: PostPreviewDto[];
}

const PostList = ({ posts }: IPostList) => {
  return (
    <>
      {posts.map(post => {
        return <Post key={post.id} post={post} />;
      })}
    </>
  );
};

export default PostList;
