export type Maybe<T> = T | null;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
};

export enum ApplyPolicy {
  BeforeResolver = "BEFORE_RESOLVER",
  AfterResolver = "AFTER_RESOLVER"
}

export type Book = {
  author?: Maybe<Scalars["String"]>;
  title?: Maybe<Scalars["String"]>;
};

export type Mutation = {
  token?: Maybe<Scalars["String"]>;
};

export type MutationTokenArgs = {
  email?: Maybe<Scalars["String"]>;
  password?: Maybe<Scalars["String"]>;
};

export type Query = {
  book?: Maybe<Book>;
};
